import { Control, FieldPath, FieldValues } from "react-hook-form";
import React from "react";

export interface IFormControl<TFieldValues extends FieldValues = FieldValues> {
    control: Control<TFieldValues>,
    name: FieldPath<TFieldValues>,
    placeholder?: string,
    label?: string,
    ref?: React.RefCallback<any>,
    autoComplete?: React.HTMLInputAutoCompleteAttribute,
}