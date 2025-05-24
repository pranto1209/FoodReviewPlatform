--
-- PostgreSQL database dump
--

-- Dumped from database version 17.4
-- Dumped by pg_dump version 17.4

SET statement_timeout = 0;
SET lock_timeout = 0;
SET idle_in_transaction_session_timeout = 0;
SET transaction_timeout = 0;
SET client_encoding = 'UTF8';
SET standard_conforming_strings = on;
SELECT pg_catalog.set_config('search_path', '', false);
SET check_function_bodies = false;
SET xmloption = content;
SET client_min_messages = warning;
SET row_security = off;

SET default_tablespace = '';

SET default_table_access_method = heap;

--
-- Name: check_in; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.check_in (
    id bigint NOT NULL,
    check_in_time timestamp with time zone NOT NULL,
    user_id bigint NOT NULL,
    restaurant_id bigint NOT NULL
);


ALTER TABLE public.check_in OWNER TO postgres;

--
-- Name: check_in_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.check_in_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.check_in_id_seq OWNER TO postgres;

--
-- Name: check_in_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.check_in_id_seq OWNED BY public.check_in.id;


--
-- Name: location; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.location (
    id bigint NOT NULL,
    name character varying(256) NOT NULL,
    latitude double precision,
    longitude double precision
);


ALTER TABLE public.location OWNER TO postgres;

--
-- Name: locations_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.locations_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.locations_id_seq OWNER TO postgres;

--
-- Name: locations_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.locations_id_seq OWNED BY public.location.id;


--
-- Name: restaurant; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.restaurant (
    id bigint NOT NULL,
    name character varying(256) NOT NULL,
    location_id bigint NOT NULL
);


ALTER TABLE public.restaurant OWNER TO postgres;

--
-- Name: restaurant_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.restaurant_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.restaurant_id_seq OWNER TO postgres;

--
-- Name: restaurant_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.restaurant_id_seq OWNED BY public.restaurant.id;


--
-- Name: review; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.review (
    id bigint NOT NULL,
    rating integer NOT NULL,
    comment text,
    review_time timestamp with time zone NOT NULL,
    user_id bigint NOT NULL,
    restaurant_id bigint NOT NULL
);


ALTER TABLE public.review OWNER TO postgres;

--
-- Name: review_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.review_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.review_id_seq OWNER TO postgres;

--
-- Name: review_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.review_id_seq OWNED BY public.review.id;


--
-- Name: role; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.role (
    id bigint NOT NULL,
    name character varying(256) NOT NULL
);


ALTER TABLE public.role OWNER TO postgres;

--
-- Name: role_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.role_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.role_id_seq OWNER TO postgres;

--
-- Name: role_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.role_id_seq OWNED BY public.role.id;


--
-- Name: user; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public."user" (
    id bigint NOT NULL,
    user_name character varying(256) NOT NULL,
    email character varying(256) NOT NULL,
    email_confirmed boolean,
    password_hash text NOT NULL,
    phone_number text,
    phone_number_confirmed boolean,
    two_factor_enabled boolean,
    insertion_time timestamp with time zone NOT NULL,
    modification_time timestamp with time zone
);


ALTER TABLE public."user" OWNER TO postgres;

--
-- Name: user_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.user_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.user_id_seq OWNER TO postgres;

--
-- Name: user_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.user_id_seq OWNED BY public."user".id;


--
-- Name: user_role; Type: TABLE; Schema: public; Owner: postgres
--

CREATE TABLE public.user_role (
    id bigint NOT NULL,
    user_id bigint NOT NULL,
    role_id bigint NOT NULL
);


ALTER TABLE public.user_role OWNER TO postgres;

--
-- Name: user_role_id_seq; Type: SEQUENCE; Schema: public; Owner: postgres
--

CREATE SEQUENCE public.user_role_id_seq
    START WITH 1
    INCREMENT BY 1
    NO MINVALUE
    NO MAXVALUE
    CACHE 1;


ALTER SEQUENCE public.user_role_id_seq OWNER TO postgres;

--
-- Name: user_role_id_seq; Type: SEQUENCE OWNED BY; Schema: public; Owner: postgres
--

ALTER SEQUENCE public.user_role_id_seq OWNED BY public.user_role.id;


--
-- Name: check_in id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.check_in ALTER COLUMN id SET DEFAULT nextval('public.check_in_id_seq'::regclass);


--
-- Name: location id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.location ALTER COLUMN id SET DEFAULT nextval('public.locations_id_seq'::regclass);


--
-- Name: restaurant id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.restaurant ALTER COLUMN id SET DEFAULT nextval('public.restaurant_id_seq'::regclass);


--
-- Name: review id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.review ALTER COLUMN id SET DEFAULT nextval('public.review_id_seq'::regclass);


--
-- Name: role id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.role ALTER COLUMN id SET DEFAULT nextval('public.role_id_seq'::regclass);


--
-- Name: user id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user" ALTER COLUMN id SET DEFAULT nextval('public.user_id_seq'::regclass);


--
-- Name: user_role id; Type: DEFAULT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_role ALTER COLUMN id SET DEFAULT nextval('public.user_role_id_seq'::regclass);


--
-- Data for Name: check_in; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.check_in VALUES (7, '2025-05-16 03:23:52.72041+06', 2, 2);
INSERT INTO public.check_in VALUES (6, '2025-05-16 03:21:46.544327+06', 3, 2);
INSERT INTO public.check_in VALUES (9, '2025-05-16 15:07:17.216681+06', 3, 3);
INSERT INTO public.check_in VALUES (12, '2025-05-17 02:28:00.632811+06', 3, 1);
INSERT INTO public.check_in VALUES (13, '2025-05-17 14:52:16.752478+06', 3, 2);
INSERT INTO public.check_in VALUES (15, '2025-05-17 21:16:48.725595+06', 2, 4);
INSERT INTO public.check_in VALUES (16, '2025-05-18 01:53:12.252136+06', 2, 5);
INSERT INTO public.check_in VALUES (17, '2025-05-18 02:42:04.548527+06', 2, 4);
INSERT INTO public.check_in VALUES (20, '2025-05-19 13:31:28.475112+06', 2, 15);
INSERT INTO public.check_in VALUES (29, '2025-05-21 17:29:28.861232+06', 2, 2);
INSERT INTO public.check_in VALUES (31, '2025-05-23 17:52:57.612127+06', 2, 19);


--
-- Data for Name: location; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.location VALUES (1, 'Mohammadpur', NULL, NULL);
INSERT INTO public.location VALUES (2, 'Dhanmondi', NULL, NULL);
INSERT INTO public.location VALUES (3, 'Mirpur', NULL, NULL);
INSERT INTO public.location VALUES (4, 'Badda', NULL, NULL);
INSERT INTO public.location VALUES (5, 'Gulshan', NULL, NULL);
INSERT INTO public.location VALUES (7, 'Uttara', NULL, NULL);
INSERT INTO public.location VALUES (8, 'Khilgaon', NULL, NULL);
INSERT INTO public.location VALUES (10, 'Baridhara', NULL, NULL);
INSERT INTO public.location VALUES (11, 'Banasree', NULL, NULL);
INSERT INTO public.location VALUES (12, 'New Market', 0, 0);
INSERT INTO public.location VALUES (9, 'Bashundhara', NULL, NULL);
INSERT INTO public.location VALUES (6, 'Banani', NULL, NULL);
INSERT INTO public.location VALUES (17, 'Lalmatia', 0, 0);


--
-- Data for Name: restaurant; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.restaurant VALUES (3, 'Chillox', 1);
INSERT INTO public.restaurant VALUES (4, 'Chillox', 2);
INSERT INTO public.restaurant VALUES (6, 'KFC', 3);
INSERT INTO public.restaurant VALUES (1, 'KFC', 1);
INSERT INTO public.restaurant VALUES (5, 'KFC', 2);
INSERT INTO public.restaurant VALUES (11, 'Khanas', 1);
INSERT INTO public.restaurant VALUES (12, 'Pizza Hut', 1);
INSERT INTO public.restaurant VALUES (13, 'Burger Xpress', 1);
INSERT INTO public.restaurant VALUES (14, 'Burger Xpress', 2);
INSERT INTO public.restaurant VALUES (15, 'Madchef', 2);
INSERT INTO public.restaurant VALUES (16, 'Kacchi Bhai', 1);
INSERT INTO public.restaurant VALUES (17, 'Sultan''s Dine', 1);
INSERT INTO public.restaurant VALUES (18, 'Sultan''s Dine', 2);
INSERT INTO public.restaurant VALUES (19, 'Burger King', 1);
INSERT INTO public.restaurant VALUES (20, 'Takeout', 1);
INSERT INTO public.restaurant VALUES (21, 'The Munch Station', 1);
INSERT INTO public.restaurant VALUES (22, 'Domino''s Pizza', 1);
INSERT INTO public.restaurant VALUES (23, 'Pizza Burg', 1);
INSERT INTO public.restaurant VALUES (2, 'BFC', 1);


--
-- Data for Name: review; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.review VALUES (4, 5, 'Great', '2025-05-16 03:24:00.538959+06', 2, 2);
INSERT INTO public.review VALUES (2, 4, 'Good', '2025-05-16 03:11:46.728978+06', 3, 2);
INSERT INTO public.review VALUES (3, 5, 'Nice', '2025-05-16 03:11:39.232889+06', 3, 2);
INSERT INTO public.review VALUES (5, 5, 'Nice', '2025-05-16 03:25:32.721927+06', 3, 3);
INSERT INTO public.review VALUES (9, 5, 'Tasty', '2025-05-21 16:59:52.11405+06', 2, 19);
INSERT INTO public.review VALUES (12, 5, 'Yummy', '2025-05-21 17:23:45.873238+06', 2, 2);
INSERT INTO public.review VALUES (25, 5, 'Yummy', '2025-05-23 18:01:54.167682+06', 5, 2);
INSERT INTO public.review VALUES (26, 5, 'Nice', '2025-05-23 18:02:06.75344+06', 5, 2);


--
-- Data for Name: role; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.role VALUES (1, 'Admin');
INSERT INTO public.role VALUES (2, 'User');


--
-- Data for Name: user; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public."user" VALUES (3, 'Pranto', 'pranto@gmail.com', NULL, '$2a$11$VUySM7WCglOWtFnCTJYWue1MfGRzPj5UoM/AMUY4c1nTAh7G5j.0q', NULL, NULL, NULL, '2025-05-16 02:54:01.390334+06', NULL);
INSERT INTO public."user" VALUES (1, 'Admin', 'admin@gmail.com', NULL, '$2a$11$chdvrIC/8PKUMBaPGo/AgOxFUu5xjOd9VSzZq0REfOUSeaSXEB3b6', NULL, NULL, NULL, '2025-05-17 16:03:54.199454+06', NULL);
INSERT INTO public."user" VALUES (2, 'Masum', 'masum@gmail.com', NULL, '$2a$11$xVMkGR3FrKbMw8XvKYm9ButpoubNYHq57DLTEgifF94RpzTSub4rK', NULL, NULL, NULL, '2025-05-16 03:23:40.790892+06', '2025-05-23 17:52:38.092294+06');


--
-- Data for Name: user_role; Type: TABLE DATA; Schema: public; Owner: postgres
--

INSERT INTO public.user_role VALUES (1, 1, 1);
INSERT INTO public.user_role VALUES (2, 2, 2);
INSERT INTO public.user_role VALUES (3, 3, 2);


--
-- Name: check_in_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.check_in_id_seq', 31, true);


--
-- Name: locations_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.locations_id_seq', 22, true);


--
-- Name: restaurant_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.restaurant_id_seq', 27, true);


--
-- Name: review_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.review_id_seq', 26, true);


--
-- Name: role_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.role_id_seq', 2, false);


--
-- Name: user_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.user_id_seq', 5, true);


--
-- Name: user_role_id_seq; Type: SEQUENCE SET; Schema: public; Owner: postgres
--

SELECT pg_catalog.setval('public.user_role_id_seq', 4, true);


--
-- Name: check_in pk_check_in; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.check_in
    ADD CONSTRAINT pk_check_in PRIMARY KEY (id);


--
-- Name: location pk_location; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.location
    ADD CONSTRAINT pk_location PRIMARY KEY (id);


--
-- Name: restaurant pk_restaurant; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.restaurant
    ADD CONSTRAINT pk_restaurant PRIMARY KEY (id);


--
-- Name: review pk_review; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.review
    ADD CONSTRAINT pk_review PRIMARY KEY (id);


--
-- Name: role pk_role; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.role
    ADD CONSTRAINT pk_role PRIMARY KEY (id);


--
-- Name: user pk_user; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public."user"
    ADD CONSTRAINT pk_user PRIMARY KEY (id);


--
-- Name: user_role pk_user_role; Type: CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_role
    ADD CONSTRAINT pk_user_role PRIMARY KEY (id);


--
-- Name: check_in fk_check_in_restaurant_restaurant_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.check_in
    ADD CONSTRAINT fk_check_in_restaurant_restaurant_id FOREIGN KEY (restaurant_id) REFERENCES public.restaurant(id) ON DELETE CASCADE;


--
-- Name: restaurant fk_restaurant_location_location_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.restaurant
    ADD CONSTRAINT fk_restaurant_location_location_id FOREIGN KEY (location_id) REFERENCES public.location(id) ON DELETE CASCADE;


--
-- Name: review fk_review_restaurant_restaurant_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.review
    ADD CONSTRAINT fk_review_restaurant_restaurant_id FOREIGN KEY (restaurant_id) REFERENCES public.restaurant(id) ON DELETE CASCADE;


--
-- Name: user_role fk_user_role_role_role_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_role
    ADD CONSTRAINT fk_user_role_role_role_id FOREIGN KEY (role_id) REFERENCES public.role(id) ON DELETE CASCADE;


--
-- Name: user_role fk_user_role_user_user_id; Type: FK CONSTRAINT; Schema: public; Owner: postgres
--

ALTER TABLE ONLY public.user_role
    ADD CONSTRAINT fk_user_role_user_user_id FOREIGN KEY (user_id) REFERENCES public."user"(id) ON DELETE CASCADE;


--
-- PostgreSQL database dump complete
--

