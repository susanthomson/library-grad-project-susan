import React from 'react';
import ReactDOM from 'react-dom';
import injectTapEventPlugin from 'react-tap-event-plugin';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import Paper from 'material-ui/Paper';
import RaisedButton from 'material-ui/RaisedButton';
import {List, ListItem} from 'material-ui/List';
import NewBookForm from './NewBookForm.js';

injectTapEventPlugin();

const baseUrl = "https://localhost:44312"

class ItemLister extends React.Component {
    constructor() {
        super();
        this.state = { books: [], reservations: [], userId: null };
    }
    
    componentDidMount() {
        this.getUserId();
        this.getData();
        this.timerID = setInterval(
            () => this.getData(),
            1000
        );
    }

    componentWillUnmount() {
        clearInterval(this.timerID);
    }

    getUserId() {
        fetch(baseUrl + "/api/users/", {credentials : "include"})
            .then(function(result) {
                return result.json();
            })
            .then(result=> {
                this.setState({userId:result});
            });    
    }

    getBooks() {
        fetch(baseUrl + "/api/books/", {credentials : "include"})
            .then(function(result) {
                return result.json();
            })
            .then(result=> {
                this.setState({books:result});
            });    
    }

    getReservations() {
        fetch(baseUrl + "/api/reservations/", {credentials : "include"})
            .then(function(result) {
                return result.json();
            })
            .then(result=> {
                this.setState({reservations:result});
            });    
    }

    getData() {
        this.getBooks();
        this.getReservations();
    } 
    
    render() {
        return(
            <div className="BookList">
                { this.state.books.map(book=> {
                    var bookReserved = this.state.reservations.filter(reservation=> {
                        return reservation.BookId === book.Id && reservation.EndDate === null;
                    });
                    var isReserved = bookReserved.length;
                    var isReservedByUser = bookReserved.filter(reservation=> {
                        return reservation.UserId === this.state.userId;    
                    }).length;
                    return (
                            <Book key={book.Id} book={book} isReserved={isReserved} isReservedByUser={isReservedByUser}/>
                    );
                }) }
                <NewBookForm
                />
            </div>
        );
    }
}

function ReserveButton(props) {
  return (
    <RaisedButton onClick={props.onClick} label="Reserve" primary={true} />
  );
}

function ReturnButton(props) {
  return (
    <RaisedButton onClick={props.onClick} label="Return" primary={true} />
  );
}

class ReserveControl extends React.Component {
  constructor(props) {
    super(props);
    this.handleReserveClick = this.handleReserveClick.bind(this);
    this.handleReturnClick = this.handleReturnClick.bind(this);
  }

  handleReserveClick() {
    fetch(baseUrl + "/api/reservations", {
            method: "POST",
            headers: {
                "Content-Type": "application/json"
            },
            body: JSON.stringify({
                "Id": this.props.bookId
            })
        })
        .then(function(result) {
            return result.json();
        })
        .then(result=> {
            console.log(result);
        });
    }

    handleReturnClick() {
        fetch(baseUrl + "/api/reservations", {
                method: "PUT",
                headers: {
                    "Content-Type": "application/json"
                },
                body: JSON.stringify({
                    "Id": this.props.bookId
                })
            })
            .then(function(result) {
                return result.json();
            })
            .then(result=> {
                console.log(result);
            }); 
    }

    render() {
        let button = null;
        if (this.props.isReservedByUser) {
            button = <ReturnButton onClick={this.handleReturnClick}/>;
        } else if (!this.props.isReserved){
            button = <ReserveButton onClick={this.handleReserveClick}/>;
        } else {
            button = <RaisedButton label="Reserved" disabled={true} />
        }

    return (
      <div>
        {button}
      </div>
    );
  }
}

function Book(props) {
    return (
        <Paper>
            <div className="Book">
                <div className="BookCoverBox">
                    <img className="BookCoverImage" src={props.book.CoverImage} />
                </div>
                <div className="BookDetails">
                    <List>
                        <ListItem primaryText={props.book.Title} secondaryText="Title" disabled={true} />
                        <ListItem primaryText={props.book.Author} secondaryText="Author" disabled={true} />
                        <ListItem primaryText={props.book.ISBN} secondaryText="ISBN" disabled={true} />
                        <ListItem primaryText={props.book.PublishDate} secondaryText="Date Published" disabled={true} />
                    </List>
                    <ReserveControl isReserved={props.isReserved} isReservedByUser={props.isReservedByUser} bookId={props.book.Id}/>
                </div>
            </div>
        </Paper>
    );
}

function Reserve(props) {
    return (
        <div className="ReserveBlock">
            <div className={props.isReserved ? "reserved" : "available"}> {props.isReserved ? "reserved" : "available"} </div>
            <ReserveControl isReserved={props.isReserved} isReservedByUser={props.isReservedByUser} bookId={props.bookId}/>
        </div>   
    );
}

const element = <MuiThemeProvider><ItemLister /></MuiThemeProvider>;

ReactDOM.render(
    element,
    document.getElementById("root")
);