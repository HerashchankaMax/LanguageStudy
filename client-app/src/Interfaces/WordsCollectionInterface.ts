import { WordInterface } from './WordInterface';

export interface WordsCollectionInterface {
    id: string;
    name: string;
    words: WordInterface[];
    description: string | null;
}