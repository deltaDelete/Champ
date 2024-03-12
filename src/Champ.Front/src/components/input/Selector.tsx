import { FieldValues } from "react-hook-form";
import { IFormControl } from "@/components/input/IFormControl.tsx";
import { FormControl, FormField, FormItem, FormLabel, FormMessage } from "@shadcn/components/ui/form.tsx";
import {
    Select,
    SelectContent,
    SelectItem,
    SelectTrigger,
    SelectValue,
} from "@shadcn/components/ui/select.tsx";

interface SelectProps<T, TFieldValues extends FieldValues = FieldValues> extends IFormControl<TFieldValues> {
    collection: T[],
    keySelector: <T>(data: T) => string,
    valueSelector: <T>(data: T) => string,
    displayNameSelector: <T>(data: T) => string,
}

export function Selector<T, TFieldValues extends FieldValues = FieldValues>(props: SelectProps<T, TFieldValues>) {
    return (
        <FormField name={props.name} control={props.control}
                   render={
                       ({ field }) => (
                           <FormItem>
                               {props.label && <FormLabel>{props.label}</FormLabel>}
                               <Select onValueChange={field.onChange} defaultValue={field.value}>
                                   <FormControl>
                                       {/* @ts-ignore */}
                                       <SelectTrigger>
                                           <SelectValue placeholder={props.placeholder} />
                                       </SelectTrigger>
                                   </FormControl>
                                   <SelectContent>
                                       {props.collection.map(value => (
                                           <SelectItem key={props.keySelector(value)}
                                                       value={props.valueSelector(value)}>{props.displayNameSelector(value)}</SelectItem>
                                       ))}
                                   </SelectContent>
                               </Select>
                               <FormMessage />
                           </FormItem>
                       )
                   } />
    );
}