import React from "react";
import TextField from "material-ui/TextField";
import FlatButton from 'material-ui/FlatButton';
import DatePicker from "material-ui/DatePicker";
import moment from "moment";
import ISBN from "isbn";

export default class NewBookForm extends React.Component {

  constructor(props) {
    super(props);
    this.state = {title: "", author: "", isbn: "", date: null};

    this.handleChange = this.handleChange.bind(this);
    this.handleDateChange = this.handleDateChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
  }

  handleChange(event) {
    this.setState({[event.target.id]: event.target.value});
  }

  handleDateChange(event, date) {
    this.setState({date: moment(date).format("D MMMM YYYY")});
  }

  handleSubmit(event) {
    alert("Book is " + this.state.title + " by " + this.state.author + " published on " + this.state.date);
    event.preventDefault();
  }

  render() {
    const isbn = window.ISBN.parse(this.state.isbn); //why is this on window?
    var validISBN = false;
    if (isbn && (isbn.isIsbn10() || isbn.isIsbn13())) {
      validISBN = true;
    }
    return (
      <form onSubmit={this.handleSubmit}>
        <TextField
          floatingLabelText="Title"
          onChange={this.handleChange}
          id="title"
        />
        <TextField
          floatingLabelText="Author"
          onChange={this.handleChange}
          id="author"
        />
        <TextField
          errorText={!validISBN && "Invalid ISBN"}
          floatingLabelText="ISBN"
          onChange={this.handleChange}
          id="isbn"
        />
        <DatePicker
          formatDate={function (date) {
            return moment(date).format("D MMMM YYYY");
          }}
          onChange={this.handleDateChange}
          id="date"
        />
        <FlatButton
          label="Submit"
          primary={true}
          onTouchTap={this.handleSubmit}
        />
      </form>
    );
  }
}