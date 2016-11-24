import React from 'react';
import ReactDOM from 'react-dom';
import injectTapEventPlugin from 'react-tap-event-plugin';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';

import BookList from './BookList.js';
import LibraryAppBar from './LibraryAppBar.js';

injectTapEventPlugin();

const baseUrl = "https://localhost:44312"

function App (props) {
    return (
        <div>
            <LibraryAppBar />
            <BookList />
        </div>
    );    
}

const element = <MuiThemeProvider><App /></MuiThemeProvider>;

ReactDOM.render(
    element,
    document.getElementById("root")
);