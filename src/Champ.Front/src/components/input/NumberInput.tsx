import { FieldValues } from "react-hook-form";
import { IFormControl } from "@/components/input/IFormControl.tsx";
import { FormControl, FormField, FormItem, FormLabel, FormMessage } from "../../../@shadcn/components/ui/form.tsx";
import { Input } from "../../../@shadcn/components/ui/input.tsx";

interface NumberInputProps<TFieldValues extends FieldValues = FieldValues> extends IFormControl<TFieldValues> {
    min?: number | string,
    max?: number | string
}

export function NumberInput<TFieldValues extends FieldValues = FieldValues>(props: NumberInputProps<TFieldValues>) {
    return (

        <FormField render={
            ({ field }) => (
                <FormItem>
                    {props.label && <FormLabel>{props.label}</FormLabel>}
                    <FormControl>
                        <Input placeholder={props.placeholder} type="number"
                               max={props.max} min={props.min}
                               {...field}
                        />
                    </FormControl>
                    <FormMessage />
                </FormItem>
            )} control={props.control} name={props.name} />
    );
}