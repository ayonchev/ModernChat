export interface ChatDetailsModel {
    id: number;
    accessType: AccessType;
    name: string;
    description: string;
    joinRequirements: string;
}

export interface ChatDetailsCreateModel {
    accessType: AccessType;
    name: string;
    description?: string;
    joinRequirements?: string;
    // public AccessType AccessType { get; set; }
    //     public string Name { get; set; }
    //     public string Description { get; set; }
    //     public string JoinRequirements { get; set; }
}

export enum AccessType {
    Private = 0,
    Public
}