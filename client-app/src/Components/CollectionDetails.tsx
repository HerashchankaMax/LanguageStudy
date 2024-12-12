import { Card, CardContent, CardDescription, CardHeader } from "semantic-ui-react";
import { CardsCollectionInterface } from "../Interfaces/CardsCollectionInterface.ts";

interface Props {
    collection: CardsCollectionInterface;
}

function CollectionDetails(props: Props) {
    const { collection } = props;
    const collectionLength = collection.wordGuids.length;

    return (
        <Card className='collection-details'>
            <CardContent>
                <CardHeader as="h1">{collection.name}</CardHeader>
                <CardDescription>
                    Number of cards in collection: {collectionLength}
                </CardDescription>
            </CardContent>
        </Card>
    );
}

export default CollectionDetails;
