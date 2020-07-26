import React, { Component } from 'react';

export class Home extends Component {
  static displayName = Home.name;

  render () {
    return (
      <div>
        <h1>TSC Technical evaluation project!</h1>
        <p>Made by R. David Mansilla</p>
        <p>Hardcoded User: David Password: 123</p>
      </div>
    );
  }
}
