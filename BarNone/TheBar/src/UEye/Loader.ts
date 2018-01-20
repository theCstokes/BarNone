declare function require(moduleNames: string[], onLoad: (...args: any[]) => void): void;

/**
 * Loader utils module for dynamically loading files.
 */
export default class Loader {
    
    /**
     * Async load file with callback.
     * @param files - files to load
     * @param callback - callback once loaded
     */
    public static async(files: string[], callback:  (...args: any[]) => void): void {
        require(files, callback);
    }

    /**
     * Sync load files 
     * @param files - files to load
     */
    public static async sync(...files: string[]): Promise<any> {
        return await new Promise<any>(function (resolve) {
            Loader.async(files, resolve);
        });
    }

    
}