import React from 'react';
import ReactDOM from 'react-dom';
import injectTapEventPlugin from 'react-tap-event-plugin';

import {grey400, grey500, grey700, greenA200} from 'material-ui/styles/colors';
import MuiThemeProvider from 'material-ui/styles/MuiThemeProvider';
import getMuiTheme from 'material-ui/styles/getMuiTheme';

import BookList from './BookList.js';
import LibraryAppBar from './LibraryAppBar.js';

injectTapEventPlugin();

const muiTheme = getMuiTheme({
  palette: {
    primary1Color: grey500,
    primary2Color: grey700,
    primary3Color: grey400,
    pickerHeaderColor: grey500,
    alternateTextColor: greenA200,
  }
});

function App (props) {
    return (
        <div>
            <LibraryAppBar />
            <BookList />
        </div>
    );    
}

const element = <MuiThemeProvider muiTheme={muiTheme}><App /></MuiThemeProvider>;

ReactDOM.render(
    element,
    document.getElementById("root")
);