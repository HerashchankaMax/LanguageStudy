import Slider from "react-slick";
import {CardsCollectionInterface} from "../Interfaces/CardsCollectionInterface.ts";
import "slick-carousel/slick/slick.css";
import "slick-carousel/slick/slick-theme.css";
import CollectionDetails from "./CollectionDetails.tsx";


interface Props {
    items: CardsCollectionInterface[];
}

interface Props {
    items: CardsCollectionInterface[];
}
function CollectionSlider({items}: Props) {
    const settings = {
        dots: true,
        fade: true,
        infinite: true,
        speed: 500,
        slidesToShow: 1,
        slidesToScroll: 1,
        waitForAnimate: false,
        align : "center",
    };
    return (
                    <Slider {...settings}>
                        {items.map((item) => (
                            <CollectionDetails key = {item.collectionName} collection={item} />
                        ))}
                    </Slider>
    );
}

export default CollectionSlider;
