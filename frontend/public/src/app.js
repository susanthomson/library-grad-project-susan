import React from 'react';
import ReactDOM from 'react-dom';
import "whatwg-fetch";

class ItemLister extends React.Component {
    constructor() {
        super();
        this.state = { items: [] };
    }
    
    componentDidMount() {
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
                <ul>
                { this.state.items.map(item=> { return (<li key={item.Id}> {item.Title} </li>); }) }
                </ul>          
            </div>  
        );
    }
}

const element = <ItemLister />;

ReactDOM.render(
    element,
    document.getElementById("root")
);