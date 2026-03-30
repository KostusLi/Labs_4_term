import React, {useState, useEffect} from 'react';
import { Button } from './Button';

export const Counter = () => {
    const[count, setCount] = useState<number>(0);

    const increase =()=>{
        if(count<5)
        {
            setCount(count+1);
        }
    }

    const reset = () =>{
        setCount(0);
    }

    useEffect(()=>{
        console.log(`Счетчик обновился. Текущее значение: ${count}`);
    }, [count]);

    return(

        <div className='counter-container'>
            <div className='display'>
                <span className={count===5?'max-value' : 'normal-value'}>
                    {count}
                </span>
            </div>

            <div className='buttons-panel'>
                <Button
                title='inc'
                callback={increase}
                disabled={count===5}
                />

                <Button
                title="reset"
                callback={reset}
                disabled={count===0}
                />
            </div>
        </div>
    );
};