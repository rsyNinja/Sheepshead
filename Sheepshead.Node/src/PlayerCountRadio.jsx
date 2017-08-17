﻿import React from 'react';

export default class PlayerCountRadio extends React.Component {
    constructor(props) {
        super(props);
        this.state = { value: props.value || 0 };
        this.handleClick = this.handleClick.bind(this);
    }

    handleClick(i) {
        this.setState({ value: i });
        if (this.props.onChange)
            this.props.onChange(this.props.name, i);
    }

    getValue() {
        return this.state.value;
    }

    renderRadio(i) {
        return (
            <span>
                <input
                    type="radio"
                    name={this.props.name}
                    value={i}
                    onClick={() => this.handleClick(i)}
                    onChange={() => function () { return; }}
                    checked={this.state.value === i}
                    disabled={parseFloat(this.props.remaining) + parseFloat(this.state.value) < i ? 'disabled' : undefined }
                />{i}
            </span>
        );
    }

    render() {
        return (
            <div className="playerCountRadio" onChange={this.props.onChange}>
                <span className="title">{this.props.title}</span>
                {this.renderRadio(0)}
                {this.renderRadio(1)}
                {this.renderRadio(2)}
                {this.renderRadio(3)}
                {this.renderRadio(4)}
                {this.renderRadio(5)}
                <input type="hidden" className="value" value={this.state.value} />
            </div>
        );
    }
}