import React from 'react';
import ReactDOM from 'react-dom';
import injectTapEventPlugin from 'react-tap-event-plugin';

import ReactTestUtils from 'react-addons-test-utils'

import {shallow, mount} from 'enzyme';
import {expect} from 'chai';
import fetchMock from 'fetch-mock';

import getMuiTheme from 'material-ui/styles/getMuiTheme';

import NewBookForm from '../public/src/NewBookForm.js';
import FlatButton from 'material-ui/FlatButton';

import moment from "moment";

describe("<NewBookForm />", () => {

  //injectTapEventPlugin();
  const url = "https://localhost:44312/api/books";
  fetchMock.post(url, 200);

  const mountWithContext = (node) => mount(node, {
    context: {
      muiTheme: getMuiTheme(),
    },
    childContextTypes: {
      muiTheme: React.PropTypes.object.isRequired,
    }
  });

  it("renders two buttons", () => {
    const wrapper = shallow(<NewBookForm />);
    expect(wrapper.find(FlatButton).length).to.equal(2);
  });

  it("submit button is disabled on mount", () => {
    const wrapper = shallow(<NewBookForm />);
    expect(wrapper.find(".submitButton").props().disabled).to.equal(true);
  });

  it("cancel button is enabled on mount", () => {
    const wrapper = shallow(<NewBookForm />);
    expect(wrapper.find(".cancelButton").props().disabled).to.equal(false);
  });

  it("start state is {book: {Title: '', Author: '', ISBN: '', CoverImage: '', PublishDate: ''}}", () => {
    const wrapper = shallow(<NewBookForm />);
    expect(wrapper.state().book.Title).to.equal("");
    expect(wrapper.state().book.Author).to.equal("");
    expect(wrapper.state().book.ISBN).to.equal("");
    expect(wrapper.state().book.CoverImage).to.equal("");
    expect(wrapper.state().book.PublishDate).to.equal("");
  });

  [
    {name: "Title", value: "Harry Potter"},
    {name: "Author", value: "Rowling"},
    {name: "ISBN", value: "1234"},
    {name: "CoverImage", value: "cover url"}
  ].forEach((field) => {
    it("changing " + field.name + " field updates state", () => {
      const wrapper = shallow(<NewBookForm />);
      expect(wrapper.state().book[field.name]).to.equal("");
      wrapper.find("#" + field.name).simulate("change", {target: {id: field.name, value: field.value}});
      expect(wrapper.state().book[field.name]).to.equal(field.value);
    });
  });

  it("changing PublishDate field updates state", () => {
    const date = Date.now();
    const formattedDate = moment(date).format("D MMMM YYYY");
    const wrapper = shallow(<NewBookForm />);
    expect(wrapper.find("#PublishDate").length).to.equal(1);
    expect(wrapper.state().book.PublishDate).to.equal("");
    wrapper.find("#PublishDate").simulate("change", null, date);
    expect(wrapper.state().book.PublishDate).to.equal(formattedDate);
  });

  it("filling in all fields and a valid ISBN enables submit button", () => {
    const date = Date.now();
    const formattedDate = moment(date).format("D MMMM YYYY");
    const wrapper = shallow(<NewBookForm />);

    expect(wrapper.find(".submitButton").props().disabled).to.equal(true);
    
    wrapper.find("#PublishDate").simulate("change", null, date);
    [
      {name: "Title", value: "Harry Potter"},
      {name: "Author", value: "Rowling"},
      {name: "ISBN", value: "0-7475-3269-9"},
      {name: "CoverImage", value: "cover url"}
    ].forEach((field) => {
      wrapper.find("#" + field.name).simulate("change", {target: {id: field.name, value: field.value}});
    });
    expect(wrapper.find(".submitButton").props().disabled).to.equal(false);
  });

  it("filling in all fields and an invalid ISBN doesn't enable submit button", () => {
    const date = new Date(0);
    const formattedDate = moment(date).format("D MMMM YYYY");
    const wrapper = shallow(<NewBookForm />);

    expect(wrapper.find(".submitButton").props().disabled).to.equal(true);
    
    wrapper.find("#PublishDate").simulate("change", null, date);
    [
      {name: "Title", value: "Harry Potter"},
      {name: "Author", value: "Rowling"},
      {name: "ISBN", value: "0-7475-3269-X"},
      {name: "CoverImage", value: "cover url"}
    ].forEach((field) => {
      wrapper.find("#" + field.name).simulate("change", {target: {id: field.name, value: field.value}});
    });
    expect(wrapper.find(".submitButton").props().disabled).to.equal(true);
  });

  it("submitting the form makes request to /api/books endpoint with book in body", () => {
    const date = Date.now();
    const formattedDate = moment(date).format("D MMMM YYYY");
    //console.log(formattedDate);
        const wrapper = mount(<NewBookForm onFinish={()=>{}}/>, {
      context: {
        muiTheme: getMuiTheme(),
      },
      childContextTypes: {
        muiTheme: React.PropTypes.object.isRequired,
      },
    });
    
    wrapper.find("#PublishDate").simulate("change", {target: {value: date}});
    [
      {name: "Title", value: "Harry Potter"},
      {name: "Author", value: "Rowling"},
      {name: "ISBN", value: "0-7475-3269-9"},
      {name: "CoverImage", value: "cover url"}
    ].forEach((field) => {
      wrapper.find("#" + field.name).simulate("change", {target: {id: field.name, value: field.value}});
    });
    const node = ReactDOM.findDOMNode(
        ReactTestUtils.findRenderedDOMComponentWithClass(
          wrapper.instance(), "submitButton"
        )
      );
    ReactTestUtils.Simulate.touchTap(node);
    expect(fetchMock.called(url)).to.equal(true);
    expect(fetchMock.lastCall(url)[1].body).to.equal(JSON.stringify(wrapper.state().book));
  });

});