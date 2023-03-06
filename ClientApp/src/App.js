import React, { useState, useRef } from "react";
import CategoryList from "./components/CategoryList";
import InputRow from "./components/InputRow";

function App() {

    const [shouldRender, setShouldRender] = useState(false);
    const nameInputRef = useRef(null);

    const handleDeleteItem = () => {
        setShouldRender(!shouldRender);
    };

    const handleAddItem = () => {
        setShouldRender(!shouldRender);
        nameInputRef.current.focus();
    };

    return (
        <div className='container square border border-2'>
            <CategoryList
                onDeleteItem={handleDeleteItem}
                shouldRender={shouldRender}
                setShouldRender={setShouldRender}
            />
            <InputRow
                onAddItem={handleAddItem}
                shouldRender={shouldRender}
                setShouldRender={setShouldRender}
                nameInputRef={nameInputRef }
            />
        </div>
    );
}

export default App;