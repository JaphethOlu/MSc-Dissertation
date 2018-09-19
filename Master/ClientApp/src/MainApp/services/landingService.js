import request from "superagent";
import prefix from "superagent-prefix";

import { base, topEmployers, topAgencies } from "./routes";

export const landingService = {
    reqTopEmployers,
    reqTopAgencies
};

function reqTopEmployers() {
    return request.get(topEmployers)
                .use(prefix(base))
                .then((res) => {
                    return res;
                });
};

function reqTopAgencies() {
    return request.get(topAgencies)
                .use(prefix(base))
                .then((res) => {
                    return res;
                });
};