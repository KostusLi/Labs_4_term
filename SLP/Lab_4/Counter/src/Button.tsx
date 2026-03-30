import React from "react";

type ButtonProps = {
    title: string;
    callback: ()=>void;
    disabled?: boolean;
};

export const Button = ({title, callback, disabled}: ButtonProps)=>{
    return(
        <button onClick={callback} 
        disabled={disabled} 
        className={`btn ${title === 'inc' ? 'btn-cyan' : 'btn-grey'}`}>
            {title}
        </button>
    );
};