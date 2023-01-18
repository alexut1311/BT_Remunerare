import React, { Component } from "react";
import { CreateModal } from "../Modal/CreateModal";

export class CreateNewPeriodModal extends Component {
  static displayName = CreateNewPeriodModal.name;

  constructor(props) {
    super(props);
    this.state = { year: 0, month: 0 };
  }

  render() {
    const setComponentState = (e) => {
      this.setState({ [e.target.name]: e.target.value });
    };
    let contents = (
      <CreateModal {...this.props} setComponentState={setComponentState} />
    );

    return <div>{contents}</div>;
  }
}
