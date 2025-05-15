-- Table: public.check_in

-- DROP TABLE IF EXISTS public.check_in;

CREATE TABLE IF NOT EXISTS public.check_in
(
    id bigint NOT NULL DEFAULT nextval('check_ins_id_seq'::regclass),
    check_in_time timestamp with time zone NOT NULL,
    user_id bigint NOT NULL,
    restaurant_id bigint NOT NULL,
    CONSTRAINT pk_check_ins PRIMARY KEY (id),
    CONSTRAINT fk_check_ins_restaurants_restaurant_id FOREIGN KEY (restaurant_id)
        REFERENCES public.restaurant (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_check_ins_users_user_id FOREIGN KEY (user_id)
        REFERENCES public."user" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.check_in
    OWNER to postgres;
-- Index: fki_fk_check_ins_restaurants_restaurant_id

-- DROP INDEX IF EXISTS public.fki_fk_check_ins_restaurants_restaurant_id;

CREATE INDEX IF NOT EXISTS fki_fk_check_ins_restaurants_restaurant_id
    ON public.check_in USING btree
    (restaurant_id ASC NULLS LAST)
    TABLESPACE pg_default;



-- Table: public.location

-- DROP TABLE IF EXISTS public.location;

CREATE TABLE IF NOT EXISTS public.location
(
    id bigint NOT NULL DEFAULT nextval('locations_id_seq'::regclass),
    area character varying(256) COLLATE pg_catalog."default" NOT NULL,
    latitude double precision,
    longitude double precision,
    CONSTRAINT pk_locations PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.location
    OWNER to postgres;


-- Table: public.restaurant

-- DROP TABLE IF EXISTS public.restaurant;

CREATE TABLE IF NOT EXISTS public.restaurant
(
    id bigint NOT NULL DEFAULT nextval('restaurant_id_seq'::regclass),
    name character varying(256) COLLATE pg_catalog."default" NOT NULL,
    location_id bigint NOT NULL,
    CONSTRAINT pk_restaurants PRIMARY KEY (id),
    CONSTRAINT fk_restaurant_locations_location_id FOREIGN KEY (location_id)
        REFERENCES public.location (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.restaurant
    OWNER to postgres;


-- Table: public.review

-- DROP TABLE IF EXISTS public.review;

CREATE TABLE IF NOT EXISTS public.review
(
    id bigint NOT NULL DEFAULT nextval('reviews_id_seq'::regclass),
    rating integer NOT NULL,
    comment text COLLATE pg_catalog."default",
    review_time timestamp with time zone NOT NULL,
    user_id bigint NOT NULL,
    restaurant_id bigint NOT NULL,
    CONSTRAINT pk_reviews PRIMARY KEY (id),
    CONSTRAINT fk_reviews_restaurants_restaurant_id FOREIGN KEY (restaurant_id)
        REFERENCES public.restaurant (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION,
    CONSTRAINT fk_reviews_users_user_id FOREIGN KEY (user_id)
        REFERENCES public."user" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE NO ACTION
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.review
    OWNER to postgres;
-- Index: fki_fk_reviews_restaurants_restaurant_id

-- DROP INDEX IF EXISTS public.fki_fk_reviews_restaurants_restaurant_id;

CREATE INDEX IF NOT EXISTS fki_fk_reviews_restaurants_restaurant_id
    ON public.review USING btree
    (restaurant_id ASC NULLS LAST)
    TABLESPACE pg_default;



-- Table: public.user

-- DROP TABLE IF EXISTS public."user";

CREATE TABLE IF NOT EXISTS public."user"
(
    id bigint NOT NULL DEFAULT nextval('users_id_seq'::regclass),
    user_name character varying(256) COLLATE pg_catalog."default" NOT NULL,
    email character varying(256) COLLATE pg_catalog."default" NOT NULL,
    email_confirmed boolean,
    password_hash text COLLATE pg_catalog."default" NOT NULL,
    phone_number text COLLATE pg_catalog."default",
    phone_number_confirmed boolean,
    insertion_time timestamp with time zone NOT NULL,
    role character varying(256) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT pk_users PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."user"
    OWNER to postgres;