declare function require(moduleNames: string[], onLoad: (...args: any[]) => void): void;

export default class Loader {
    private static fileCache: { [key: string]: any } = {};

    public static async(files: string[], callback:  (...args: any[]) => void): void {
        require(files, callback);
    }

    public static async sync(...files: string[]): Promise<any> {
        return await new Promise<any>(function (resolve) {
            Loader.async(files, resolve);
        });
    }

    
}