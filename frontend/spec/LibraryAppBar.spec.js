import React from 'react';
import ReactDOM from 'react-dom';
import injectTapEventPlugin from 'react-tap-event-plugin';

import ReactTestUtils from 'react-addons-test-utils'

import {shallow, mount} from 'enzyme';
import {expect} from 'chai';

import getMuiTheme from 'material-ui/styles/getMuiTheme';

import LibraryAppBar from '../public/src/LibraryAppBar.js';
import AppBar from 'material-ui/AppBar';
import IconButton from 'material-ui/IconButton';
import AlertWarning from 'material-ui/svg-icons/alert/warning';
import Dialog from 'material-ui/Dialog';

describe("<LibraryAppBar />", () => {

  injectTapEventPlugin();

  const mountWithContext = (node) => mount(node, {
    context: {
      muiTheme: getMuiTheme(),
    },
    childContextTypes: {
      muiTheme: React.PropTypes.object.isRequired,
    }
  });

  it("renders an <AppBar /> component", () => {
    const wrapper = shallow(<LibraryAppBar />);
    expect(wrapper.find(AppBar).length).to.equal(1);
  });

  it("start state is {open: false, showAmazingHeader: false}", () => {
    const wrapper = shallow(<LibraryAppBar />);
    expect(wrapper.state().open).to.equal(false);
    expect(wrapper.state().showAmazingHeader).to.equal(false);
  });

  it("clicking icon button shows amazing header", () => {
    const wrapper = mount(<LibraryAppBar />, {
      context: {
        muiTheme: getMuiTheme(),
      },
      childContextTypes: {
        muiTheme: React.PropTypes.object.isRequired,
      },
    });
    expect(wrapper.state().showAmazingHeader).to.equal(false);
    expect(wrapper.find("marquee").length).to.equal(0);
    const node = ReactDOM.findDOMNode(
        ReactTestUtils.findRenderedDOMComponentWithClass(
          wrapper.instance(), "showHeaderButton"
        )
      );
    ReactTestUtils.Simulate.touchTap(node);
    expect(wrapper.state().showAmazingHeader).to.equal(true);
    expect(wrapper.find("marquee").length).to.equal(1);
  });

  it("clicking add book button opens new book dialog", () => {
    const wrapper = mount(<LibraryAppBar />, {
      context: {
        muiTheme: getMuiTheme(),
      },
      childContextTypes: {
        muiTheme: React.PropTypes.object.isRequired,
      },
    });
    expect(wrapper.state().open).to.equal(false);
    expect(wrapper.find(Dialog).props().open).to.equal(false);
    const node = ReactDOM.findDOMNode(
        ReactTestUtils.findRenderedDOMComponentWithClass(
          wrapper.instance(), "addBookButton"
        )
      );
    ReactTestUtils.Simulate.touchTap(node);
    expect(wrapper.state().open).to.equal(true);
    expect(wrapper.find(Dialog).props().open).to.equal(true);
  });
});