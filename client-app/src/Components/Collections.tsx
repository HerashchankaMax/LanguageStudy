import {Button, Grid} from "semantic-ui-react";
import CollectionsSlider from "./CollectionsSlider.tsx";
import {CardsCollectionInterface} from "../Interfaces/CardsCollectionInterface.ts";
import axios from "axios";
import {useEffect, useState} from "react";
import axiosInstance from "../axiosInstance.ts";
import CreateCollection from "./CreateCollection.tsx";

function Collections() {

    const [collections, setCollections] = useState<CardsCollectionInterface[]>([]);
    const [activeCollection, setActiveCollection] = useState<CardsCollectionInterface | undefined>( undefined);
    const [activeComponent, setActiveComponent] = useState<string>();
    useEffect(() => {
        try{
            axiosInstance
                .get<CardsCollectionInterface[]>("http://localhost:5000/api/collections")
                .then(response => {
                    setCollections(response.data);
                });
        }
        catch (e) {
            if(axios.isAxiosError(e)){
                console.log(e.response?.data);
            }
            console.log(e);
        }
    }, [] )

    function renderActiveComponent() {
        if(activeComponent === 'create') {
            return <CreateCollection  />
        }
        if(activeComponent === 'edit') {
            return <CreateCollection collection={activeCollection}/>
        }
        if(activeComponent === 'list') {
            return <div>List component</div>
        }
    }

    return (
        <Grid >
            <Grid.Row >
                <Grid.Column width={16} style={{padding : 0}}>
                    <CollectionsSlider items={collections} onActiveCollectionChange={setActiveCollection}></CollectionsSlider>
                </Grid.Column>
            </Grid.Row>
            <Grid.Row>
                <Grid.Column width={16} style={{padding : 0, display:"flex", justifyContent: "center"}}>
                    <Button onClick={()=> setActiveComponent('create')}>Create new collection</Button>
                    <Button onClick={() => setActiveComponent('edit')}>Edit collection</Button>
                    <Button onClick={()=> setActiveComponent('list')}>See list of collections</Button>
                </Grid.Column>
            </Grid.Row>
            <Grid.Row>
                <Grid.Column width={16} style={ {padding : 0}}>
                    {renderActiveComponent()}
                </Grid.Column>
            </Grid.Row>
        </Grid>
    );
}

export default Collections;