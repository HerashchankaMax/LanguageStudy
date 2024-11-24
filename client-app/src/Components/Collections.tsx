import {Button, Grid} from "semantic-ui-react";
import CollectionsSlider from "./CollectionsSlider.tsx";
import {CardsCollectionInterface} from "../Interfaces/CardsCollectionInterface.ts";
import {CardInterface} from "../Interfaces/CardInterface.ts";

function Collections() {

    const card1: CardInterface = {
        id: "1",
        word: "Privet",
        translation: "Hello",
        dateCreated: new Date(),
        dateModified: new Date(),
    };
    const card2: CardInterface = {
        id: "2",
        word: "Mama",
        translation: "Mother",
        dateCreated: new Date(),
        dateModified: new Date(),
    }
    const card3: CardInterface = {
        id: "3",
        word: "Papa",
        translation: "Father",
        dateCreated: new Date(),
        dateModified: new Date(),
    }
    const initialCards : CardInterface[] = [card1, card2];

    const initialCards2 : CardInterface[] = [card2, card1, card3];

    let cardsCollection : CardsCollectionInterface ={
        collectionName : 'Test collection',
        cards : initialCards,
        totalViews : 1
    }

    let cardsCollection2 : CardsCollectionInterface ={
        collectionName : 'Test collection 2',
        cards : initialCards2,
        totalViews : 2
    }
    return (
        <Grid >
            <Grid.Row >
                <Grid.Column width={16} style={{padding : 0}}>
                    <CollectionsSlider items={[cardsCollection, cardsCollection2]}></CollectionsSlider>
                </Grid.Column>
            </Grid.Row>
            <Grid.Row>
                <Grid.Column width={16} style={{padding : 0, display:"flex", justifyContent: "center"}}>
                    <Button>Create new collection</Button>
                    <Button>Edit collection</Button>
                    <Button>See list of collections</Button>
                </Grid.Column>
            </Grid.Row>
        </Grid>
    );
}

export default Collections;