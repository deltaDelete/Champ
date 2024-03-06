import "./card.css";
import React, {ReactElement} from "react";
import {List} from "linqts";

type CardProps = {
    children: ReactElement<ICardContent> | ReactElement<ICardContent>[]
}

type ICardContent = {}

type CardHeaderProps = ICardContent & {
    header: string
}

type CardBody = ICardContent & {
    children: ReactElement | ReactElement[]
}

export function Card({children}: CardProps) {
    const header = () => {
        if (Array.isArray(children)) {
            const childrenList = new List<ReactElement<ICardContent>>();
            childrenList.AddRange(children);
            return childrenList.Where((x) => ("header" in x!.props)).First();
        }
        return undefined;
    }
    const body = () => {
        if (Array.isArray(children)) {
            const childrenList = new List<ReactElement<ICardContent>>();
            childrenList.AddRange(children);
            return childrenList.Where(x => ("children" in x!.props));
        }
        return undefined;
    }
    return (
        <div className="card">
            {header()}
            {body()}
        </div>
    );
}

export function CardHeader(props: CardHeaderProps) {
    return (
        <p className="header">{props.header}</p>
    )
}

export function CardBody(props: CardBody) {
    return (
        <div className="card-body">
            {props.children}
        </div>
    )
}