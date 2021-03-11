
export const getImgPath = {
    methods: {
        getImgPath(url, resize = true, type = 0) {
            if(url.startsWith("http")){
                return url
            }
            if (resize) {
                //这个地址换成你自己的oss地址
                return 'https://dotnetcore.oss-cn-shanghai.aliyuncs.com' + url + '?x-oss-process=image/resize,w_200';
            } else {
                return 'https://dotnetcore.oss-cn-shanghai.aliyuncs.com' + url
            }
        }
    }
}
