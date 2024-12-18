import React, {useEffect, useState} from 'react';
import { Grid, Button } from 'semantic-ui-react';
import InputRow from './InputRow';
import { WordsCollectionInterface } from '../Interfaces/WordsCollectionInterface';

interface Props {
    collection?: WordsCollectionInterface;
}

export interface RowData {
    word: string;
    translation: string;
    definition: string;
}

const CreateCollection: React.FC<Props> = ({ collection }) => {

    const [rows, setRows] = useState<RowData[]>([{"word" : '', "translation" : '', "definition" : ''}]);

    const handleInputChange = (index: number, field: string, value: string) => {
        const newRows = [...rows];
        newRows[index] = { ...newRows[index], [field]: value };
        setRows(newRows);
    };

    console.log(collection?.words.length);
    useEffect(() => {
        if (collection) {
            const mappedRows = collection.words.map((word) => {
                return {
                    word: word.value,
                    translation: word.translation,
                    definition: word.definition
                };
            })
            setRows(mappedRows);
        }
        else {
            setRows([{ word: '', translation: '', definition: '' }]);
        }
    }, [collection]);
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