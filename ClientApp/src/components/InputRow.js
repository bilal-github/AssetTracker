import React, { useState, useEffect } from "react";

function InputRow(props) {
    const [itemName, setItemName] = useState("");
    const [itemValue, setItemValue] = useState(0);
    const [categoryId, setCategoryId] = useState("");
    const [categories, setCategories] = useState([]);

    useEffect(() => {
        fetch("api/categories")
            .then((response) => response.json())
            .then((data) => {
                setCategories(data);
                setCategoryId(data[0].categoryId);
            })
            .catch((error) => console.log(error));
    }, [props.shouldRender]);

    const handleNameChange = (event) => {
        setItemName(event.target.value);
    };

    const handleValueChange = (event) => {
        setItemValue(parseFloat(event.target.value));
    };

    const handleCategoryChange = (event) => {
        setCategoryId(event.target.value);
    };

    const handleSubmit = (event) => {
        event.preventDefault();

        const formData = {
            itemName,
            value: itemValue,
            categoryId,
        };

        fetch("api/items", {
            method: "POST",
            headers: {
                "Content-Type": "application/json",
            },
            body: JSON.stringify(formData),
        })
            .then((response) => response.json())
            .then((data) => {
                console.log(data);
                console.log(categories);
                onAddItem(data);
                setItemName("");
                setItemValue(0);
            })
            .catch((error) => console.log(error));
    };

    const onAddItem = (newItem) => {
        props.onAddItem(newItem);
    };

   return (
        <form onSubmit={handleSubmit} className="row">
            <div className="col-3">
                <input
                    type="text"
                    className="form-control"
                    placeholder="Item Name"
                    value={itemName}
                    onChange={handleNameChange}
                    required
                />
            </div>
            <div className="col-3">
                <div className="input-group">
                    <span className="input-group-text">$</span>
                    <input
                        type="number"
                        className="form-control"
                        placeholder="Value"
                        value={itemValue}
                        onChange={handleValueChange}
                        required
                    />
                </div>
            </div>
            <div className="col-3">
                <select
                    className="form-select"
                    value={categoryId}
                    onChange={handleCategoryChange}
                    required
                >
                    <option value="">Select a Category</option>
                    {categories.map((category) => (
                        <option key={category.categoryId} value={category.categoryId}>
                            {category.categoryName}
                        </option>
                    ))}
                </select>
            </div>
            <div className="col-3">
                <button type="submit" className="btn btn-primary">
                    Add Item
                </button>
            </div>
        </form>
    );
}

export default InputRow;
