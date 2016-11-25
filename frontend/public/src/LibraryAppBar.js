import React from 'react';
import Dialog from 'material-ui/Dialog';
import FlatButton from 'material-ui/FlatButton';
import IconButton from 'material-ui/IconButton';
import AlertWarning from 'material-ui/svg-icons/alert/warning';
import AppBar from 'material-ui/AppBar';
import {pinkA100} from 'material-ui/styles/colors';


import NewBookForm from './NewBookForm.js';

export default class LibraryAppBar extends React.Component {

    constructor(props) {
        super(props);
        this.state = {open: false, showAmazingHeader: false};

        this.handleOpen = this.handleOpen.bind(this);
        this.handleClose = this.handleClose.bind(this);
        this.handleShowAmazingHeader = this.handleShowAmazingHeader.bind(this);
    }

  handleOpen() {
    this.setState({open: true});
  };

  handleClose () {
    this.setState({open: false});
  };

  handleShowAmazingHeader() {
    const show = !this.state.showAmazingHeader;
    this.setState({showAmazingHeader: show});
  };

  render() {   
    return (
      <div>
        {this.state.showAmazingHeader && (
          <div className="Book">
            <img className="spin" src="https://upload.wikimedia.org/wikipedia/commons/5/50/British_Longhair_-_Black_Silver_Shaded.jpg"/>
            <marquee><h2>Library Library Library</h2></marquee>
            <img className="spin" src="https://upload.wikimedia.org/wikipedia/commons/5/50/British_Longhair_-_Black_Silver_Shaded.jpg"/>
          </div>)}
        <AppBar
            title="Library"
            iconElementLeft={<IconButton onTouchTap={this.handleShowAmazingHeader}><AlertWarning color={pinkA100}/></IconButton>}
            iconElementRight={<FlatButton label="Add Book" onTouchTap={this.handleOpen} />}
        />
        <Dialog
          title="New Book"
          modal={false}
          open={this.state.open}
          onRequestClose={this.handleClose}
        >
          <NewBookForm onFinish={this.handleClose.bind(this)}/>
        </Dialog>
      </div>
    );
  }

}