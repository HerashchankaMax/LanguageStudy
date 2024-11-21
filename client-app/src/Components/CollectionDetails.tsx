import { Card, CardContent, CardDescription, CardHeader } from "semantic-ui-react";
import { CardsCollectionInterface } from "../Interfaces/CardsCollectionInterface.ts";

interface Props {
    collection: CardsCollectionInterface;
}

function CollectionDetails(props: Props) {
    const { collection } = props;
    const collectionLength = collection.cards.length;

    return (
        <Card className='collection-details'>
            <CardContent>
                <CardHeader as="h1">{collection.collectionName}</CardHeader>
                <CardDescription>
                    Number of cards in collection: {collectionLength}
                </CardDescription>
                <CardDescription>
                    Last update: {collection.cards[0]?.dateModified.toLocaleDateString() || "N/A"}
                </CardDescription>
            </CardContent>
        </Card>
    );
}

export default CollectionDetails;
