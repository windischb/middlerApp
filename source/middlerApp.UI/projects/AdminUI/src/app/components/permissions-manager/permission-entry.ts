export class PermissionEntry {
    public Id: string;
    public Order: number;
    public PrincipalName: string;
    public Type: string;
    public AccessMode: string;
    public Client: string;
    public SourceAddress: string;
}

export function PermissionEntrySortByOrder(a: PermissionEntry, b: PermissionEntry) {
    if (a.Order < b.Order) {
        return -1;
    }
    if (a.Order > b.Order) {
        return 1;
    }
    return 0;
}