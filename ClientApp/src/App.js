import React, { useState } from "react";
import CategoryList from "./components/CategoryList";
import InputRow from "./components/InputRow";

function App() {

    const [shouldRender, setShouldRender] = useState(false);

    const handleDeleteItem = () => {
        setShouldRender(!shouldRender);
    };

    const handleAddItem = () => {
        setShouldRender(!shouldRender);
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
            />
        </div>
    );
}

export default App;