import { Component, ComponentState } from "react";
import "./card.css";

export function Card({children}) {
    return (
        <div className="card">
            {children}
        </div>
    );
}

export class CardHeader extends Component<CardHeaderProps, ComponentState> {
    render() {
        return (
            <p className="header">{this.props.header}</p>
        )
    }
}

interface CardHeaderProps {
    header: string
}