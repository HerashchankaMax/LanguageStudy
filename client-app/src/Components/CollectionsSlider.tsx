import Slider from "react-slick";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import CollectionDetails from "./CollectionDetails.tsx";
import { WordsCollectionInterface } from "../Interfaces/WordsCollectionInterface.ts";


interface Props {
    items: WordsCollectionInterface[];
}

interface Props {
    items: WordsCollectionInterface[];
    onActiveCollectionChange: (collection: WordsCollectionInterface) => void;

}
function CollectionsSlider({items, onActiveCollectionChange}: Props) {
    const settings = {
        dots: true,
        fade: true,
        infinite: true,
        speed: 1000,
        slidesToShow: 1,
        slidesToScroll: 1,
        waitForAnimate: false,
        align : "center",
        afterChange : (currentSlide: number) => onActiveCollectionChange(items[currentSlide])
    };
    return (
            <Slider {...settings}>
                {items.map((item) => (
                    <CollectionDetails key = {item.name} collection={item} />
                ))}
            </Slider>
    );
}

export default CollectionsSlider;

