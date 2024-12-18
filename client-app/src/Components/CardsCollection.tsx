import { WordInterface } from "../Interfaces/WordInterface.ts";
import { WordsCollectionInterface } from "../Interfaces/WordsCollectionInterface.ts";
import Card from "./Card.tsx";
import {Header, List} from "semantic-ui-react";


function CardsCollection(collection : WordsCollectionInterface) {
    return (
        <div className="container-fluid">
            <Header as="h1" content="Cards Collection"/>
            <List >
                {
                    collection.words.map((card: WordInterface) => (
                        <List.Item key={card.id}>
                            <Card instance={card}></Card>
                        </List.Item>
                    ))
                }
            </List>
        </div>
    )
}

export default CardsCollection;