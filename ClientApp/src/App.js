import React, { useState } from "react";
import CategoryList from "./components/CategoryList";


function App() {

    const [shouldRender, setShouldRender] = useState(false);

    const handleDeleteItem = () => {
        setShouldRender(!shouldRender);
    };

    return (
        <div className='container square border border-2'>
            <CategoryList
                onDeleteItem={handleDeleteItem}
                shouldRender={shouldRender}
                setShouldRender={setShouldRender}
            />
        </div>
    );
}

export default App;