// import { useMutation, useQuery, useQueryClient } from "@tanstack/react-query";
// import { deleteFileByName, uploadFile as onUploadFile, viewFile } from "../../src/helpers/backend_helper";


// export const useDownLoadFile = (fileName: string) => {
//     return useQuery({
//         queryKey: ['fileList', fileName],
//         queryFn: async () => await viewFile(fileName),
//         enabled: !!fileName,
//     });
// };  
// export const useUploadFile = () => {
//     const qc = useQueryClient();
//     return useMutation({
//         mutationFn: (file: File) => onUploadFile(file),
//         onSuccess: () => { 
//         qc.invalidateQueries({ queryKey: ["fileList"] });  
//         },
//     });
// };

// export const useDeleteFile = () => {
//     const qc = useQueryClient();
//     return useMutation({
//         mutationFn: (localName: string) => deleteFileByName(localName),
//         onSuccess: () => {
//         qc.invalidateQueries({ queryKey: ["fileList"] }); 
//         },
//     });
// }; 

