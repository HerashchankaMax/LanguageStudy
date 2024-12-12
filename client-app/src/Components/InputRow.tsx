import React from 'react';
import { Grid, Input } from 'semantic-ui-react';

interface InputRowProps {
    index: number;
    word?: string;
    translation?: string;
    definition?: string;
    onChange: (index: number, field: string, value: string) => void;
}

const InputRow: React.FC<InputRowProps> = ({ index, word = '', translation = '', definition = '', onChange }) => {
    return (
        <Grid.Row>
            <Grid.Column>
                <Input
                    placeholder="Word"
                    value={word}
                    onChange={(e) => onChange(index, 'word', e.target.value)}
                />
            </Grid.Column>
            <Grid.Column>
                <Input
                    placeholder="Translation"
                    value={translation}
                    onChange={(e) => onChange(index, 'translation', e.target.value)}
                />
            </Grid.Column>
            <Grid.Column>
                <Input
                    placeholder="Definition"
                    value={definition}
                    onChange={(e) => onChange(index, 'definition', e.target.value)}
                />
            </Grid.Column>
        </Grid.Row>
    );
};

export default InputRow;