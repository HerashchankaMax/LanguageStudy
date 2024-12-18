import { Button, Grid } from "semantic-ui-react";
import CollectionsSlider from "./CollectionsSlider.tsx";
import axios from "axios";
import { useEffect, useState } from "react";
import axiosInstance from "../axiosInstance.ts";
import CreateCollection from "./CreateCollection.tsx";
import { WordsCollectionInterface } from "../Interfaces/WordsCollectionInterface.ts";

function Collections() {
    const [collections, setCollections] = useState<WordsCollectionInterface[]>([]);
    const [activeCollection, setActiveCollection] = useState<WordsCollectionInterface | undefined>(undefined);
    const [activeComponent, setActiveComponent] = useState<string>();

    useEffect(() => {
        try {
            axiosInstance
                .get<WordsCollectionInterface[]>("http://localhost:5000/api/collections")
                .then(response => {
                    setCollections(response.data);
                });
        } catch (e) {
            if (axios.isAxiosError(e)) {
                console.log(e.response?.data);
            }
            console.log(e);
        }
    }, []);

    function renderActiveComponent() {
        if (activeComponent === 'create') {
            console.log("create")
            return <CreateCollection collection ={undefined} />;
        }
        if (activeComponent === 'edit') {
            console.log( activeCollection?.words.length);
            return <CreateCollection collection={activeCollection} />;
        }
        if (activeComponent === 'list') {
            return <div>List component</div>;
        }
        return null;
    }

    return (
        <Grid>
            <Grid.Row>
                <Grid.Column width={16} style={{ padding: 0 }}>
                    <CollectionsSlider items={collections}
                                       onActiveCollectionChange={setActiveCollection}></CollectionsSlider>
                </Grid.Column>
            </Grid.Row>
            <Grid.Row>
                <Grid.Column width={16} style={{ padding: 0, display: "flex", justifyContent: "center" }}>
                    <Button onClick={() => {
                        setActiveCollection(undefined);
                        setActiveComponent('create');
                    }}>Create new collection</Button>
                    <Button onClick={() => setActiveComponent('edit')}>Edit collection</Button>
                    <Button onClick={() => setActiveComponent('list')}>See list of collections</Button>
                </Grid.Column>
            </Grid.Row>
            <Grid.Row>
                <Grid.Column width={16} style={{ padding: 0 }}>
                    {renderActiveComponent()}
                </Grid.Column>
            </Grid.Row>
        </Grid>
    );
}

export default Collections;