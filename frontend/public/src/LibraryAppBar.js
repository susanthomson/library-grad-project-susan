import React from 'react';
import Dialog from 'material-ui/Dialog';
import FlatButton from 'material-ui/FlatButton';
import AppBar from 'material-ui/AppBar';

import NewBookForm from './NewBookForm.js';

export default class LibraryAppBar extends React.Component {

    constructor(props) {
        super(props);
        this.state = {open: false};

        this.handleOpen = this.handleOpen.bind(this);
        this.handleClose = this.handleClose.bind(this);
    }

  handleOpen() {
    this.setState({open: true});
  };

  handleClose () {
    this.setState({open: false});
  };

  render() {

    return (
      <div>
        <AppBar
            title="Library"
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