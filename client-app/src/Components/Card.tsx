import { WordInterface } from "../Interfaces/WordInterface";

export default function ({instance}: { instance: WordInterface }) {
    return (
        <div>
            <div className="card-header"></div>
            <div className="card-body">{instance.value}</div>
            <div className="card-footer"></div>
        </div>
    )
}