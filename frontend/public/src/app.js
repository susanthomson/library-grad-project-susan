import React from 'react';
import ReactDOM from 'react-dom';
import "whatwg-fetch";

class ItemLister extends React.Component {
    constructor() {
        super();
        this.state = { items: [] };
    }
    
    componentDidMount() {
        this.getBooks();
        this.timerID = setInterval(
            () => this.getBooks(),
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
                this.setState({items:result});
            });    
    }
    
    render() {
        return(
            <div>
                <div>Books:</div>
                <div className="BookList">
                    { this.state.items.map(item=> { return (<Book key={item.Id} book={item} />); }) }
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