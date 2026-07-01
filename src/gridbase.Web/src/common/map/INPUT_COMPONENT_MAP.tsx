import React from "react"; 
import dayjs from 'dayjs';
import DatePicker from "antd/es/date-picker"; 
import isoWeek from "dayjs/plugin/isoWeek"; 
import {VideoManager} from "../../pages/DatatableItem/RenderCellInput/components/VideoManagerInput";
import quarterOfYear from "dayjs/plugin/quarterOfYear"; 
import FileManager from "../../pages/DatatableItem/RenderCellInput/components/FileInput"; 
import { DataType } from "common/enums/DataType";
import { InputTypeEnum } from "common/enums/inputTypeEnum"; 
import { TextOrTelInput } from "../../pages/DatatableItem/RenderCellInput/components/TextOrTelInput";
import { TextareaInput } from "../../pages/DatatableItem/RenderCellInput/components/TextareaInput";
import { AlertInput } from "../../pages/DatatableItem/RenderCellInput/components/AlertInput";
import { QRCodeInput } from "../../pages/DatatableItem/RenderCellInput/components/QRCodeInput"; 
import { RatingsInput } from "../../pages/DatatableItem/RenderCellInput/components/RatingsInput";
import { BadgeInput } from "../../pages/DatatableItem/RenderCellInput/components/BadgeInput";
import { RangeDatetimeLocalInput } from "../../pages/DatatableItem/RenderCellInput/components/RangeDatetimeLocalInput";
import { RangeQuarterInput } from "../../pages/DatatableItem/RenderCellInput/components/RangeQuarterInput";
import { RangeWeekInput } from "../../pages/DatatableItem/RenderCellInput/components/RangeWeekInput";
import { RangeYearInput } from "../../pages/DatatableItem/RenderCellInput/components/RangeYearInput";
import { RangeMonthInput } from "../../pages/DatatableItem/RenderCellInput/components/RangeMonthInput";
import { MultipleTimeInput } from "../../pages/DatatableItem/RenderCellInput/components/MultipleTimeInput";
import { MultipleDateInput } from "../../pages/DatatableItem/RenderCellInput/components/MultipleDateInput";
import { DatetimeLocalInput } from "../../pages/DatatableItem/RenderCellInput/components/DatetimeLocalInput";
import { QuarterInput } from "../../pages/DatatableItem/RenderCellInput/components/QuarterInput";
import { YearInput } from "../../pages/DatatableItem/RenderCellInput/components/YearInput";
import { MonthInput } from "../../pages/DatatableItem/RenderCellInput/components/MonthInput";
import { WeekInput } from "../../pages/DatatableItem/RenderCellInput/components/WeekInput";
import { DateInput } from "../../pages/DatatableItem/RenderCellInput/components/DateInput";
import { TimeInput } from "../../pages/DatatableItem/RenderCellInput/components/TimeInput";
import { NumberInput } from "../../pages/DatatableItem/RenderCellInput/components/NumberInput";
import { EmailInput } from "../../pages/DatatableItem/RenderCellInput/components/EmailInput";
import { CheckboxInput } from "../../pages/DatatableItem/RenderCellInput/components/CheckboxInput";
import { RadioInput } from "../../pages/DatatableItem/RenderCellInput/components/RadioInput";
import { RangeInput } from "../../pages/DatatableItem/RenderCellInput/components/RangeInput"; 
import { RangeDateInput } from "../../pages/DatatableItem/RenderCellInput/components/RangeDateInput";
import { SelectInput } from "../../pages/DatatableItem/RenderCellInput/components/SelectInput";
import { HtmlEditorInput } from "../../pages/DatatableItem/RenderCellInput/components/HtmlEditorInput";
import { DropManager } from "../../pages/DatatableItem/RenderCellInput/components/DropInput";
import { ImageUpload } from "../../pages/DatatableItem/RenderCellInput/components/ImageUploadInput";
import { ViewImage } from "../../pages/DatatableItem/RenderCellInput/components/ViewImageInput";
import { HtmlInput } from "../../pages/DatatableItem/RenderCellInput/components/HtmlInput";
import { ForeignColumnInput } from "../../pages/DatatableItem/RenderCellInput/components/ForeignColumnInput";
import { BadgesInput } from "pages/DatatableItem/RenderCellInput/components/BadgesInput";
import { UsersInput } from "pages/DatatableItem/RenderCellInput/components/UsersInput";
import { IconInput } from "pages/DatatableItem/RenderCellInput/components/IconInput";
import { ParentInput } from "pages/DatatableItem/RenderCellInput/components/ParentInput";
import { SwitchInput } from "pages/DatatableItem/RenderCellInput/components/SwitchInput";

dayjs.extend(quarterOfYear)
dayjs.extend(isoWeek);
const { RangePicker } = DatePicker;
const HtmlEditorWrapper  = (p: any) => p.isEditRow ? <TextareaInput extra={p.extra} /> : <HtmlEditorInput {...p} />;
const HtmlWrapper        = (p: any) => p.isEditRow ? <TextareaInput extra={p.extra} /> : <HtmlInput {...p} />;
const DropFilesWrapper = React.forwardRef((p: any, ref: any) => 
    p.isEditRow 
        ? <FileManager {...p} ref={ref} /> 
        : <DropManager {...p} ref={ref} />
);
const ImageWrapper = React.forwardRef((p: any, ref: any) => {
    if (p.isEditRow)                   
        return <FileManager {...p} ref={ref} />;
    if (p.modalType == DataType.View || p.modalType == DataType.List) 
        return <ViewImage value={p.value} />;
    return <ImageUpload {...p} ref={ref} />;
}); 
const ForeignColumnWrapper = (p: any) => (
    <ForeignColumnInput
        {...p}
        cells={p.cells?.[p.col?.realColumnId ?? 0]}
    />
);

export const INPUT_COMPONENT_MAP: Record<string, React.ComponentType<any>> = {
    [InputTypeEnum.ForeignColumn.toLowerCase()]: ForeignColumnWrapper,
    [InputTypeEnum.HtmlEditor.toLowerCase()]: HtmlEditorWrapper,  
    [InputTypeEnum.Html.toLowerCase()]:       HtmlWrapper,        
    [InputTypeEnum.DropFiles.toLowerCase()]:  DropFilesWrapper,   
    [InputTypeEnum.Image.toLowerCase()]:      ImageWrapper,       
    [InputTypeEnum.File.toLowerCase()]:       FileManager,
    [InputTypeEnum.Video.toLowerCase()]:      VideoManager,
    [InputTypeEnum.Select.toLowerCase()]:     SelectInput,
    [InputTypeEnum.Checkbox.toLowerCase()]:   CheckboxInput,
    [InputTypeEnum.Radio.toLowerCase()]:      RadioInput,
    [InputTypeEnum.Range.toLowerCase()]:      RangeInput,
    [InputTypeEnum.Email.toLowerCase()]:      EmailInput,
    [InputTypeEnum.Number.toLowerCase()]:     NumberInput,
    [InputTypeEnum.Time.toLowerCase()]:       TimeInput,
    [InputTypeEnum.Date.toLowerCase()]:       DateInput,
    [InputTypeEnum.Week.toLowerCase()]:       WeekInput,
    [InputTypeEnum.Month.toLowerCase()]:      MonthInput,
    [InputTypeEnum.Year.toLowerCase()]:       YearInput,
    [InputTypeEnum.Quarter.toLowerCase()]:    QuarterInput,
    [InputTypeEnum.DatetimeLocal.toLowerCase()]:      DatetimeLocalInput,
    [InputTypeEnum.MultipleDate.toLowerCase()]:       MultipleDateInput,
    [InputTypeEnum.MultipleTime.toLowerCase()]:       MultipleTimeInput,
    [InputTypeEnum.RangeDate.toLowerCase()]:          RangeDateInput,
    [InputTypeEnum.RangeMonth.toLowerCase()]:         RangeMonthInput,
    [InputTypeEnum.RangeYear.toLowerCase()]:          RangeYearInput,
    [InputTypeEnum.RangeWeek.toLowerCase()]:          RangeWeekInput,
    [InputTypeEnum.RangeQuarter.toLowerCase()]:       RangeQuarterInput,
    [InputTypeEnum.RangeDatetimeLocal.toLowerCase()]: RangeDatetimeLocalInput,
    [InputTypeEnum.Ratings.toLowerCase()]:   RatingsInput,
    [InputTypeEnum.QRCode.toLowerCase()]:    QRCodeInput,
    [InputTypeEnum.Badge.toLowerCase()]:     BadgeInput,
    [InputTypeEnum.Alert.toLowerCase()]:     AlertInput,
    [InputTypeEnum.Textarea.toLowerCase()]:  TextareaInput,
    [InputTypeEnum.Text.toLowerCase()]:      TextOrTelInput,
    [InputTypeEnum.Tel.toLowerCase()]:       TextOrTelInput,
    [InputTypeEnum.Badges.toLowerCase()]: BadgesInput, 
    [InputTypeEnum.User.toLowerCase()]:       UsersInput,
    [InputTypeEnum.Icon.toLowerCase()]:       IconInput,
    [InputTypeEnum.Parent.toLowerCase()]:       ParentInput,
    [InputTypeEnum.Switch.toLowerCase()]: SwitchInput, 
};