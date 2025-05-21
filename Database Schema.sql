-- Table: public.check_in

-- DROP TABLE IF EXISTS public.check_in;

CREATE TABLE IF NOT EXISTS public.check_in
(
    id bigint NOT NULL DEFAULT nextval('check_in_id_seq'::regclass),
    check_in_time timestamp with time zone NOT NULL,
    user_id bigint NOT NULL,
    restaurant_id bigint NOT NULL,
    CONSTRAINT pk_check_in PRIMARY KEY (id),
    CONSTRAINT fk_check_in_restaurant_restaurant_id FOREIGN KEY (restaurant_id)
        REFERENCES public.restaurant (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.check_in
    OWNER to postgres;


-- Table: public.location

-- DROP TABLE IF EXISTS public.location;

CREATE TABLE IF NOT EXISTS public.location
(
    id bigint NOT NULL DEFAULT nextval('locations_id_seq'::regclass),
    name character varying(256) COLLATE pg_catalog."default" NOT NULL,
    latitude double precision,
    longitude double precision,
    CONSTRAINT pk_location PRIMARY KEY (id)
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
    CONSTRAINT pk_restaurant PRIMARY KEY (id),
    CONSTRAINT fk_restaurant_location_location_id FOREIGN KEY (location_id)
        REFERENCES public.location (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.restaurant
    OWNER to postgres;


-- Table: public.review

-- DROP TABLE IF EXISTS public.review;

CREATE TABLE IF NOT EXISTS public.review
(
    id bigint NOT NULL DEFAULT nextval('review_id_seq'::regclass),
    rating integer NOT NULL,
    comment text COLLATE pg_catalog."default",
    review_time timestamp with time zone NOT NULL,
    user_id bigint NOT NULL,
    restaurant_id bigint NOT NULL,
    CONSTRAINT pk_review PRIMARY KEY (id),
    CONSTRAINT fk_review_restaurant_restaurant_id FOREIGN KEY (restaurant_id)
        REFERENCES public.restaurant (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.review
    OWNER to postgres;


-- Table: public.role

-- DROP TABLE IF EXISTS public.role;

CREATE TABLE IF NOT EXISTS public.role
(
    id bigint NOT NULL DEFAULT nextval('role_id_seq'::regclass),
    name character varying(256) COLLATE pg_catalog."default" NOT NULL,
    CONSTRAINT pk_role PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.role
    OWNER to postgres;


-- Table: public.user

-- DROP TABLE IF EXISTS public."user";

CREATE TABLE IF NOT EXISTS public."user"
(
    id bigint NOT NULL DEFAULT nextval('user_id_seq'::regclass),
    user_name character varying(256) COLLATE pg_catalog."default" NOT NULL,
    email character varying(256) COLLATE pg_catalog."default" NOT NULL,
    email_confirmed boolean,
    password_hash text COLLATE pg_catalog."default" NOT NULL,
    phone_number text COLLATE pg_catalog."default",
    phone_number_confirmed boolean,
    two_factor_enabled boolean,
    insertion_time timestamp with time zone NOT NULL,
    modification_time timestamp with time zone,
    CONSTRAINT pk_user PRIMARY KEY (id)
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public."user"
    OWNER to postgres;


-- Table: public.user_role

-- DROP TABLE IF EXISTS public.user_role;

CREATE TABLE IF NOT EXISTS public.user_role
(
    id bigint NOT NULL DEFAULT nextval('user_role_id_seq'::regclass),
    user_id bigint NOT NULL,
    role_id bigint NOT NULL,
    CONSTRAINT pk_user_role PRIMARY KEY (id),
    CONSTRAINT fk_user_role_role_role_id FOREIGN KEY (role_id)
        REFERENCES public.role (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE,
    CONSTRAINT fk_user_role_user_user_id FOREIGN KEY (user_id)
        REFERENCES public."user" (id) MATCH SIMPLE
        ON UPDATE NO ACTION
        ON DELETE CASCADE
)

TABLESPACE pg_default;

ALTER TABLE IF EXISTS public.user_role
    OWNER to postgres;


