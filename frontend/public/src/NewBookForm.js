import React from "react";
import TextField from "material-ui/TextField";
import FlatButton from 'material-ui/FlatButton';
import DatePicker from "material-ui/DatePicker";
import moment from "moment";
import ISBN from "isbn";

export default class NewBookForm extends React.Component {

  constructor(props) {
    super(props);
    this.state = {book: {Title: "", Author: "", ISBN: "", CoverImage: "", PublishDate: ""}};

    this.handleChange = this.handleChange.bind(this);
    this.handleDateChange = this.handleDateChange.bind(this);
    this.handleSubmit = this.handleSubmit.bind(this);
    this.handleCancel = this.handleCancel.bind(this);

    this.baseUrl = "https://localhost:44312";
  }

  handleChange(event) {
    const property = event.target.id;
    const value = event.target.value;
    this.setState(previousState=>{
      previousState.book[property] = value;
      return previousState
    });
  }

  handleDateChange(event, date) {
    this.setState(previousState=>{
      previousState.book.PublishDate = moment(date).format("D MMMM YYYY");
      return previousState
    });
  }

  handleSubmit(event) {
      fetch(this.baseUrl + "/api/books", {
          method: "POST",
          headers: {
              "Content-Type": "application/json"
          },
          body: JSON.stringify(this.state.book)
      })
      .then(function(result) {
          return result.json();
      })
      .then(result=> {
          console.log(result);
      });
    this.props.onFinish();
    event.preventDefault();
  }

  handleCancel(event) {
    this.props.onFinish();
    event.preventDefault();
  }

  render() {
    const bookISBN = window.ISBN.parse(this.state.book.ISBN); //why is this on window?
    var validISBN = false;
    if (bookISBN && (bookISBN.isIsbn10() || bookISBN.isIsbn13())) {
      validISBN = true;
    }
    const validBook = validISBN && Object.keys(this.state.book).every(
      (key) => this.state.book[key]);
    return (
      <div>
        <div className="Book">
          <div className="BookCoverBox">
              <img className="BookCoverImage" src={this.state.book.CoverImage || "defaultCover.svg"} />
          </div>       
          <div className="BookDetails">
            <TextField
              floatingLabelText="Title"
              onChange={this.handleChange}
              id="Title"
            /><br />
            <TextField
              floatingLabelText="Author"
              onChange={this.handleChange}
              id="Author"
            /><br />
            <TextField
              errorText={!validISBN && "Invalid ISBN"}
              floatingLabelText="ISBN"
              onChange={this.handleChange}
              id="ISBN"
            /><br />
            <DatePicker
              hintText="Publication Date"
              formatDate={function (date) {
                return moment(date).format("D MMMM YYYY");
              }}
              onChange={this.handleDateChange}
              id="PublishDate"
            /><br />
            <TextField
              floatingLabelText="Cover Image"
              onChange={this.handleChange}
              id="CoverImage"
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