import { Button, List, ListItem, TextField } from '@material-ui/core';
import React, { useState } from 'react';
import { useQuery } from "react-query";
import { lookupBarcode } from './barcode-lookup-query';
import { LookupResponse } from './barcode-loolup-model';

const BarcodeLookup = () => {
    const [barcode, onBarcodeChanged] = useState("");

    const { isLoading, error, data, refetch } = useQuery<LookupResponse[], Error>(
        "lookupBarcode",
        () => lookupBarcode(import.meta.env.VITE_API_URL ?? '', barcode)
    );

    const lookup = async (e: React.FormEvent) => {
        e.preventDefault();

        refetch();
    }

    return <>
        <form onSubmit={e => lookup(e)}>
            <TextField type="text" value={barcode} onChange={e => onBarcodeChanged(e.target.value)} />
            <Button variant='contained' type="submit" >Lookup</Button>
        </form>
        {data &&
            <ul>
                <LookupResultList result={data} />
            </ul>
        }
    </>
}

const LookupResultList: React.FC<{ result: LookupResponse[] }> = ({ result }) => <List>
    {result.map(d => <ListItem key={d.id}>{d.title}</ListItem>)}
</List>;

export default BarcodeLookup;