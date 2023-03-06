import React, { useState, useEffect } from "react";
import ItemList from "./ItemList";

function CategoryList(props) {
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        fetch("api/item")
            .then((response) => response.json())
            .then((data) => setCategories(data))
            .catch((error) => console.log(error));
    }, [props.shouldRender]);

    const calculateTotal = (items) => {
        return items.reduce((total, item) => total + item.value, 0);
    };

    const calculateTotalValue = () => {
        let total = 0;
        Object.values(categories).forEach((items) => {
            total += calculateTotal(items);
        });
        return total;
    };

    const handleDeleteItem = (itemId) => {
        const updatedCategories = { ...categories };
        Object.keys(updatedCategories).forEach((categoryName) => {
            updatedCategories[categoryName] = updatedCategories[categoryName].filter(
                (item) => item.itemId !== itemId
            );
        });
        setCategories(updatedCategories);
    };

    const categoriesWithItems = Object.keys(categories).filter(
        categoryName => categories[categoryName].length > 0
    );

    return (
        <div>
            {categoriesWithItems.map((categoryName) => (
                <div className="row mb-3" key={categoryName}>
                    <div className="col-4">
                        <h3>{categoryName}</h3>
                    </div>
                    <div className="col-8 text-start">
                        <h3>${calculateTotal(categories[categoryName]).toFixed(2)}</h3>
                    </div>
                    <div className="col-12">
                        <ItemList
                            items={categories[categoryName]}
                            onDeleteItem={handleDeleteItem}
                        />
                    </div>
                </div>
            ))}
            <div className="row mb-3">
                <div className="col-4">
                    <h3>Total</h3>
                </div>
                <div className="col-8 text-start">
                    <h3>${calculateTotalValue().toFixed(2)}</h3>
                </div>
            </div>
        </div>
    );
}

export default CategoryList;