import React from "react";
import TextField from "material-ui/TextField";
import FlatButton from 'material-ui/FlatButton';
import DatePicker from "material-ui/DatePicker";
import moment from "moment";
import ISBN from "isbn";

export default class NewBookForm extends React.Component {

  constructor(props) {
    super(props);
    this.state = {title: "", author: "", isbn: "", cover: "", date: null};

    this.handleChange = this.handleChange.bind(this);
    this.handleDateChange = this.handleDateChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleCancel = this.handleCancel.bind(this);
  }

  handleChange(event) {
    this.setState({[event.target.id]: event.target.value});
  }

  handleDateChange(event, date) {
    this.setState({date: moment(date).format("D MMMM YYYY")});
  }

  handleSubmit(event) {
    this.props.onFinish();
    alert("Book is " + this.state.title + " by " + this.state.author + " published on " + this.state.date);
    event.preventDefault();
  }

  handleCancel(event) {
    this.props.onFinish();
    event.preventDefault();
  }

  render() {
    const isbn = window.ISBN.parse(this.state.isbn); //why is this on window?
    var validISBN = false;
    if (isbn && (isbn.isIsbn10() || isbn.isIsbn13())) {
      validISBN = true;
    }
    const validBook = validISBN && Object.keys(this.state).every(
      (key) => this.state[key]);
    return (
      <div>
        <div className="Book">
          <div className="BookCoverBox">
              <img className="BookCoverImage" src={this.state.cover || "defaultCover.svg"} />
          </div>       
          <div className="BookDetails">
            <TextField
              floatingLabelText="Title"
              onChange={this.handleChange}
              id="title"
            /><br />
            <TextField
              floatingLabelText="Author"
              onChange={this.handleChange}
              id="author"
            /><br />
            <TextField
              errorText={!validISBN && "Invalid ISBN"}
              floatingLabelText="ISBN"
              onChange={this.handleChange}
              id="isbn"
            /><br />
            <DatePicker
              hintText="Publication Date"
              formatDate={function (date) {
                return moment(date).format("D MMMM YYYY");
              }}
              onChange={this.handleDateChange}
              id="date"
            /><br />
            <TextField
              floatingLabelText="Cover Image"
              onChange={this.handleChange}
              id="cover"
            />
          </div>
        </div>
        <div className="right-justify">
          <FlatButton
            label="Cancel"
            primary={true}
            onTouchTap={this.handleCancel}
          />
          <FlatButton
            label="Submit"
            primary={true}
            onTouchTap={this.handleSubmit}
            disabled={!validBook}
          />
        </div>
      </div>
    );
  }
}