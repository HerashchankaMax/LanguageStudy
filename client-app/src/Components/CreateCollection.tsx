import React, { useState } from 'react';
import { Grid, Button } from 'semantic-ui-react';
import { CardsCollectionInterface } from '../Interfaces/CardsCollectionInterface';
import InputRow from './InputRow';

interface Props {
    collection?: CardsCollectionInterface;
}

export interface RowData {
    word: string;
    translation: string;
    definition: string;
}

const CreateCollection: React.FC<Props> = ({ collection }) => {
    const initialRows = collection
        ? collection.wordGuids.map((guid) => ({ word: guid, translation: '', definition: '' }))
        : [{ word: '', translation: '', definition: '' }];

    const [rows, setRows] = useState<RowData[]>(initialRows);

    const handleInputChange = (index: number, field: string, value: string) => {
        const newRows = [...rows];
        newRows[index][field] = value;
        setRows(newRows);
    };

    const addNewRow = () => {
        setRows([...rows, { word: '', translation: '', definition: '' }]);
    };

    return (
        <div>
            <h1>{collection ? "Edit Collection" : "Create New Collection"}</h1>
            <Grid columns={3} divided>
                {rows.map((row, index) => (
                    <InputRow
                        key={index}
                        index={index}
                        word={row.word}
                        translation={row.translation}
                        definition={row.definition}
                        onChange={handleInputChange}
                    />
                ))}
            </Grid>
            <Button onClick={addNewRow}>Add New Row</Button>
        </div>
    );
};

export default CreateCollection;