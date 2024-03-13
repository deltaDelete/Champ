import { FieldValues } from "react-hook-form";
import { FormControl, FormField, FormItem, FormLabel, FormMessage } from "@shadcn/components/ui/form.tsx";
import { Input } from "@shadcn/components/ui/input.tsx";
import { IFormControl } from "@/components/input/IFormControl.tsx";

interface TextInputProps<TFieldValues extends FieldValues = FieldValues> extends IFormControl<TFieldValues> {
}
export function TextInput<TFieldValues extends FieldValues = FieldValues>(props: TextInputProps<TFieldValues>) {
    return (
        <FormField render={
            ({ field }) => (
                <FormItem ref={props.ref}>
                    {props.label && <FormLabel>{props.label}</FormLabel>}
                    <FormControl>
                        <Input autoComplete={props.autoComplete} placeholder={props.placeholder} {...field} />
                    </FormControl>
                    <FormMessage />
                </FormItem>
            )} control={props.control} name={props.name} />
    );
}