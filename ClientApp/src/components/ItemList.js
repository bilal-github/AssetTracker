import React, { useState, useEffect } from "react";
import "../styles/styles.css";

function ItemList(props) {
    const [items, setItems] = useState(props.items);

    useEffect(() => {
        setItems(props.items);
    }, [props.items]);

    const handleDelete = (itemId) => {
        fetch(`api/item/${itemId}`, { method: "DELETE" })
            .then((response) => response.json())
            .then((data) => {
                console.log(data);
                fetch("api/item")
                    .then((response) => response.json())
                    .then((data) => {
                        console.log(data);
                        setItems(data);
                        props.onDeleteItem(itemId);
                    })
                    .catch((error) => console.log(error));
            })
            .catch((error) => console.log(error));
    };
    return (
        <ul className="category-list">
            {props.items.map((item) => (
                <li className="row mb-3" key={item.itemId}>
                    <div className="col-4">
                        {item.itemName}
                    </div>
                    <div className="col-4">
                        ${item.value.toFixed(2)}
                        <span className="inline" onClick={() => handleDelete(item.itemId)}>Trash
                        </span>
                    </div>
                </li>
            ))}
        </ul>
    );
}

export default ItemList;
