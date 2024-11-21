import {CardInterface} from "./CardInterface.ts";

export interface CardsCollectionInterface{
    collectionName: string;
    cards : CardInterface[]
    modifyingDate : Date;
    totalViews : number;
}
