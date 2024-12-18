import { Card, CardContent, CardDescription, CardHeader } from "semantic-ui-react";
import { WordsCollectionInterface } from "../Interfaces/WordsCollectionInterface";

interface Props {
    collection: WordsCollectionInterface;
}

function CollectionDetails(props: Props) {
    const { collection } = props;
    const collectionLength = collection.words.length;

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
