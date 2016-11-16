import React from 'react';
import ReactDOM from 'react-dom';
import "whatwg-fetch";

class ItemLister extends React.Component {
    constructor() {
        super();
        this.state = { books: [], reservations: [] };
    }
    
    componentDidMount() {
        this.getData();
        this.timerID = setInterval(
            () => {this.getData},
            1000
        );
    }

    componentWillUnmount() {
        clearInterval(this.timerID);
    }

    getBooks() {
        fetch("http://localhost:51918/api/books/")
            .then(function(result) {
                return result.json();
            })
            .then(result=> {
                this.setState({books:result});
            });    
    }

    getReservations() {
        fetch("http://localhost:51918/api/reservations/")
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
                    { this.state.books.map(item=> {
                        var reserved = this.state.reservations.filter(reservation=> {
                            return reservation.BookId === item.Id && reservation.EndDate === null;
                        }).length ? "reserved" : "available";
                        return (
                            <div className="ReservedBook" key={item.Id}>
                                <Book book={item} />
                                <div className={reserved}> {reserved} </div>
                            </div>
                        );
                    }) }
                </div>  
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

const element = <ItemLister />;

ReactDOM.render(
    element,
    document.getElementById("root")
);