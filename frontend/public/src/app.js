import React from 'react';
import ReactDOM from 'react-dom';

const baseUrl = "https://localhost:44312"

class ItemLister extends React.Component {
    constructor() {
        super();
        this.state = { books: [], reservations: [] };
    }
    
    componentDidMount() {
        this.getData();
        this.timerID = setInterval(
            () => this.getData(),
            1000
        );
    }

    componentWillUnmount() {
        clearInterval(this.timerID);
    }

    getBooks() {
        fetch(baseUrl + "/api/books/")
            .then(function(result) {
                return result.json();
            })
            .then(result=> {
                this.setState({books:result});
            });    
    }

    getReservations() {
        fetch(baseUrl + "/api/reservations/")
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
            <div>
                <div>Books:</div>
                <div className="BookList">
                    { this.state.books.map(book=> {
                        var isReserved = this.state.reservations.filter(reservation=> {
                            return reservation.BookId === book.Id && reservation.EndDate === null;
                        }).length;
                        return (
                            <div className="ReservedBook" key={book.Id}>
                                <Book book={book} />
                                <Reserve isReserved={isReserved} bookId={book.Id}/>
                            </div>
                        );
                    }) }
                </div>  
            </div>  
        );
    }
}

function ReserveButton(props) {
  return (
    <button onClick={props.onClick}>
      Reserve
    </button>
  );
}

function ReturnButton(props) {
  return (
    <button onClick={props.onClick}>
      Return
    </button>
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
        if (this.props.isReserved) {
            button = <ReturnButton onClick={this.handleReturnClick}/>;
        } else {
            button = <ReserveButton onClick={this.handleReserveClick} bookId={this.props.bookId} />;
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
        <div className="Book">
            <div>{props.book.ISBN}</div>
            <div>{props.book.Title}</div>
            <div>{props.book.Author}</div>
            <div>{props.book.PublishDate}</div>
        </div>
    );
}

function Reserve(props) {
    return (
        <div className="ReserveBlock">
            <div className={props.isReserved ? "reserved" : "available"}> {props.isReserved ? "reserved" : "available"} </div>
            <ReserveControl isReserved={props.isReserved} bookId={props.bookId}/>
        </div>   
    );
}

const element = <ItemLister />;

ReactDOM.render(
    element,
    document.getElementById("root")
);