import {CardInterface} from "../Interfaces/CardInterface.ts";

export default function ({instance}: { instance: CardInterface }) {
    return (
        <div>
            <div className="card-header"></div>
            <div className="card-body">{instance.word}</div>
            <div className="card-footer"></div>
        </div>
    )
}