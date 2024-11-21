import {CardInterface} from "../Interfaces/CardInterface.ts";
import Card from "./Card.tsx";
import {Header, List} from "semantic-ui-react";
import {CardsCollectionInterface} from "../Interfaces/CardsCollectionInterface.ts";


function CardsCollection(collection : CardsCollectionInterface) {
    return (
        <div className="container-fluid">
            <Header as="h1" content="Cards Collection"/>
            <List >
                {
                    collection.cards.map((card: CardInterface) => (
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